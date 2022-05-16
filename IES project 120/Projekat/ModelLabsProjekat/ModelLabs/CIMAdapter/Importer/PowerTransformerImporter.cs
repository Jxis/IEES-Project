using System;
using System.Collections.Generic;
using CIM.Model;
using FTN.Common;
using FTN.ESI.SIMES.CIM.CIMAdapter.Manager;

namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	/// <summary>
	/// PowerTransformerImporter
	/// </summary>
	public class PowerTransformerImporter
	{
		/// <summary> Singleton </summary>
		private static PowerTransformerImporter ptImporter = null;
		private static object singletoneLock = new object();

		private ConcreteModel concreteModel;
		private Delta delta;
		private ImportHelper importHelper;
		private TransformAndLoadReport report;


		#region Properties
		public static PowerTransformerImporter Instance
		{
			get
			{
				if (ptImporter == null)
				{
					lock (singletoneLock)
					{
						if (ptImporter == null)
						{
							ptImporter = new PowerTransformerImporter();
							ptImporter.Reset();
						}
					}
				}
				return ptImporter;
			}
		}

		public Delta NMSDelta
		{
			get 
			{
				return delta;
			}
		}
		#endregion Properties


		public void Reset()
		{
			concreteModel = null;
			delta = new Delta();
			importHelper = new ImportHelper();
			report = null;
		}

		public TransformAndLoadReport CreateNMSDelta(ConcreteModel cimConcreteModel)
		{
			LogManager.Log("Importing PowerTransformer Elements...", LogLevel.Info);
			report = new TransformAndLoadReport();
			concreteModel = cimConcreteModel;
			delta.ClearDeltaOperations();

			if ((concreteModel != null) && (concreteModel.ModelMap != null))
			{
				try
				{
					// convert into DMS elements
					ConvertModelAndPopulateDelta();
				}
				catch (Exception ex)
				{
					string message = string.Format("{0} - ERROR in data import - {1}", DateTime.Now, ex.Message);
					LogManager.Log(message);
					report.Report.AppendLine(ex.Message);
					report.Success = false;
				}
			}
			LogManager.Log("Importing PowerTransformer Elements - END.", LogLevel.Info);
			return report;
		}

		/// Method performs conversion of network elements from CIM based concrete model into DMS model.
		private void ConvertModelAndPopulateDelta()
		{
			LogManager.Log("Loading elements and creating delta...", LogLevel.Info);

            //// import all concrete model types (DMSType enum)
            // bitna mi redosled kojim se generisu globalni identifikatori 
            ImportDisconnectors();
            ImportOutageSchedules();
            ImportIrregularTimePoints();
            ImportRegularIntervalSchedule();
            ImportRegularTimePoints();
            ImportCurves();
            ImportCurveDatas();

            LogManager.Log("Loading elements and creating delta completed.", LogLevel.Info);
		}

        // samo za konkretne klase 
        // kreiranje ResourceDescription instanci na osnovu CIM objekta 
        #region Import

        // REGULAR INTERVAL SCHEDULE
        private ResourceDescription CreateRegularIntervalScheduleDescription(FTN.RegularIntervalSchedule cimRegularIntervalSchedule)
        {
            ResourceDescription rd = null;

            if (cimRegularIntervalSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARINTERVALSCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.REGULARINTERVALSCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegularIntervalSchedule.ID, gid);

                // pozivam populate za regular interval schedule
                PowerTransformerConverter.PopulateRegularIntervalScheduleProperties(cimRegularIntervalSchedule, rd);
            }

            return rd;
        }

        // selektuju listu CIM objekata i za svaki objekat iz liste se kreira ResourceDescription i instanca se popunjava Property podacima 
        private void ImportRegularIntervalSchedule()
        {
            // Izvuce mi sve objekte iz xmla 
            SortedDictionary<string, object> cimRegularIntervalSchedules = concreteModel.GetAllObjectsOfType("FTN.RegularIntervalSchedule");

            if (cimRegularIntervalSchedules != null)
            {
                foreach (KeyValuePair<string, object> cimBRegularIntervalSchedulePair in cimRegularIntervalSchedules)
                {
                    FTN.RegularIntervalSchedule cimRegularIntervalSchedule = cimBRegularIntervalSchedulePair.Value as FTN.RegularIntervalSchedule;

                    ResourceDescription rd = CreateRegularIntervalScheduleDescription(cimRegularIntervalSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularIntervalSchedule ID = ").Append(cimRegularIntervalSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("cimRegularIntervalSchedule ID = ").Append(cimRegularIntervalSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }

                report.Report.AppendLine();
            }
        }

        // REGULAR TIME POINT
        private ResourceDescription CreateRegularTimePointDescription(FTN.RegularTimePoint cimRegularTimePoint)
        {
            ResourceDescription rd = null;
            if (cimRegularTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.REGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.REGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimRegularTimePoint.ID, gid);

                PowerTransformerConverter.PopulateRegularTimePointProperties(cimRegularTimePoint, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportRegularTimePoints()
        {
            SortedDictionary<string, object> cimRegularTimePoints = concreteModel.GetAllObjectsOfType("FTN.RegularTimePoint");

            if (cimRegularTimePoints != null)
            {
                foreach (KeyValuePair<string, object> cimRegularTimePointPair in cimRegularTimePoints)
                {
                    FTN.RegularTimePoint cimRegularTimePoint = cimRegularTimePointPair.Value as FTN.RegularTimePoint;

                    ResourceDescription rd = CreateRegularTimePointDescription(cimRegularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("RegularTimePoint ID = ").Append(cimRegularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        // IRREGULAR TIME POINT
        private ResourceDescription CreateIrregularTimePointDescription(FTN.IrregularTimePoint cimIrregularTimePoint)
        {
            ResourceDescription rd = null;
            if (cimIrregularTimePoint != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.IRREGULARTIMEPOINT, importHelper.CheckOutIndexForDMSType(DMSType.IRREGULARTIMEPOINT));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimIrregularTimePoint.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateIrregularTimePointProperties(cimIrregularTimePoint, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportIrregularTimePoints()
        {
            SortedDictionary<string, object> cimIrregularTimePoints = concreteModel.GetAllObjectsOfType("FTN.IrregularTimePoint");

            if (cimIrregularTimePoints != null)
            {
                foreach (KeyValuePair<string, object> cimIrregularTimePointPair in cimIrregularTimePoints)
                {
                    FTN.IrregularTimePoint cimIrregularTimePoint = cimIrregularTimePointPair.Value as FTN.IrregularTimePoint;

                    ResourceDescription rd = CreateIrregularTimePointDescription(cimIrregularTimePoint);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimIrregularTimePoint.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("IrregularTimePoint ID = ").Append(cimIrregularTimePoint.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        // OUTAGE SCHEDULE
        private ResourceDescription CreateOutageScheduleDescription(FTN.OutageSchedule cimOutageSchedule)
        {
            ResourceDescription rd = null;
            if (cimOutageSchedule != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.OUTAGESCHEDULE, importHelper.CheckOutIndexForDMSType(DMSType.OUTAGESCHEDULE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimOutageSchedule.ID, gid);

                PowerTransformerConverter.PopulateOutageScheduleProperties(cimOutageSchedule, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportOutageSchedules()
        {
            SortedDictionary<string, object> cimOutageSchedules = concreteModel.GetAllObjectsOfType("FTN.OutageSchedule");

            if (cimOutageSchedules != null)
            {
                foreach (KeyValuePair<string, object> cimOutageSchedulePair in cimOutageSchedules)
                {
                    FTN.OutageSchedule cimOutageSchedule = cimOutageSchedulePair.Value as FTN.OutageSchedule;

                    ResourceDescription rd = CreateOutageScheduleDescription(cimOutageSchedule);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("OutageSchedule ID = ").Append(cimOutageSchedule.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        // DISCONNECTOR
        private ResourceDescription CreateDisconnectorDescription(FTN.Disconnector cimDisconnector)
        {
            ResourceDescription rd = null;
            if (cimDisconnector != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.DISCONNECTOR, importHelper.CheckOutIndexForDMSType(DMSType.DISCONNECTOR));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimDisconnector.ID, gid);

                PowerTransformerConverter.PopulateDisconnectorProperties(cimDisconnector, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportDisconnectors()
        {
            SortedDictionary<string, object> cimDisconnectors = concreteModel.GetAllObjectsOfType("FTN.Disconnector");

            if (cimDisconnectors != null)
            {
                foreach (KeyValuePair<string, object> cimDisconnectorPair in cimDisconnectors)
                {
                    FTN.Disconnector cimDisconnector = cimDisconnectorPair.Value as FTN.Disconnector;

                    ResourceDescription rd = CreateDisconnectorDescription(cimDisconnector);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Disconnector ID = ").Append(cimDisconnector.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Disconnector ID = ").Append(cimDisconnector.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
      
        // CURVE 
        private ResourceDescription CreateCurveDescription(FTN.Curve cimCurve)
        {
            ResourceDescription rd = null;
            if (cimCurve != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVE, importHelper.CheckOutIndexForDMSType(DMSType.CURVE));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimCurve.ID, gid);

                PowerTransformerConverter.PopulateCurveProperties(cimCurve, rd);
            }
            return rd;
        }

        private void ImportCurves()
        {
            SortedDictionary<string, object> cimCurves = concreteModel.GetAllObjectsOfType("FTN.Curve");

            if (cimCurves != null)
            {
                foreach (KeyValuePair<string, object> cimCurvePair in cimCurves)
                {
                    FTN.Curve cimCurve = cimCurvePair.Value as FTN.Curve;

                    ResourceDescription rd = CreateCurveDescription(cimCurve);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("Curve ID = ").Append(cimCurve.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("Curve ID = ").Append(cimCurve.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }

        // CURVE DATA
        private ResourceDescription CreateCurveDataDescription(FTN.CurveData cimCurveData)
        {
            ResourceDescription rd = null;
            if (cimCurveData != null)
            {
                long gid = ModelCodeHelper.CreateGlobalId(0, (short)DMSType.CURVEDATA, importHelper.CheckOutIndexForDMSType(DMSType.CURVEDATA));
                rd = new ResourceDescription(gid);
                importHelper.DefineIDMapping(cimCurveData.ID, gid);

                ////populate ResourceDescription
                PowerTransformerConverter.PopulateCurveDataProperties(cimCurveData, rd, importHelper, report);
            }
            return rd;
        }

        private void ImportCurveDatas()
        {
            SortedDictionary<string, object> cimCurveDatas = concreteModel.GetAllObjectsOfType("FTN.CurveData");

            if (cimCurveDatas != null)
            {
                foreach (KeyValuePair<string, object> cimCurveDataPair in cimCurveDatas)
                {
                    FTN.CurveData cimCurveData = cimCurveDataPair.Value as FTN.CurveData;

                    ResourceDescription rd = CreateCurveDataDescription(cimCurveData);
                    if (rd != null)
                    {
                        delta.AddDeltaOperation(DeltaOpType.Insert, rd, true);
                        report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).Append(" SUCCESSFULLY converted to GID = ").AppendLine(rd.Id.ToString());
                    }
                    else
                    {
                        report.Report.Append("CurveData ID = ").Append(cimCurveData.ID).AppendLine(" FAILED to be converted");
                    }
                }
                report.Report.AppendLine();
            }
        }
        #endregion Import
    }
}

