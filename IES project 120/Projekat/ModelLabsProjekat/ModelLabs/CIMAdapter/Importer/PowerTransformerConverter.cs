namespace FTN.ESI.SIMES.CIM.CIMAdapter.Importer
{
	using FTN.Common;

	/// <summary>
	/// PowerTransformerConverter has methods for populating
	/// ResourceDescription objects using PowerTransformerCIMProfile_Labs objects.
	/// </summary>
	public static class PowerTransformerConverter
	{

        #region Populate METODE

        // Ostaje mi isto 
        // IDENTIFIED OBJECT
        public static void PopulateIdentifiedObjectProperties(FTN.IdentifiedObject cimIdentifiedObject, ResourceDescription rd)
        {
            if ((cimIdentifiedObject != null) && (rd != null))
            {
                if (cimIdentifiedObject.MRIDHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_MRID, cimIdentifiedObject.MRID));
                }
                if (cimIdentifiedObject.NameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_NAME, cimIdentifiedObject.Name));
                }
                if (cimIdentifiedObject.AliasNameHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IDOBJ_DESCRIPTION, cimIdentifiedObject.AliasName));
                }
            }
        }

        // BASIC INTERVAL SCHEDULE
        public static void PopulateBasicIntervlScheduleProperties(FTN.BasicIntervalSchedule cimBasicIntervalSchedule, ResourceDescription rd)
        {
            if ((cimBasicIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimBasicIntervalSchedule, rd);

                if (cimBasicIntervalSchedule.StartTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_STARTTIME, cimBasicIntervalSchedule.StartTime));
                }
                if (cimBasicIntervalSchedule.Value1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value1Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value1Unit)));
                }
                if (cimBasicIntervalSchedule.Value2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER, (short)GetDMSUnitMultiplier(cimBasicIntervalSchedule.Value2Multiplier)));
                }
                if (cimBasicIntervalSchedule.Value2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT, (short)GetDMSUnitSymbol(cimBasicIntervalSchedule.Value2Unit)));
                }
            }
        }

        // IRREGULAR INTERVAL SCHEDULE
        public static void PopulateIrregularIntervalScheduleProperties(FTN.IrregularIntervalSchedule cimIrregularIntervalSchedule, ResourceDescription rd)
        {
            if ((cimIrregularIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateBasicIntervlScheduleProperties(cimIrregularIntervalSchedule, rd);

            }

            // Ne stavljam referencu jer je lista 
        }

        // RELUGAR ITNERVAL SCHEDULE
        public static void PopulateRegularIntervalScheduleProperties(FTN.RegularIntervalSchedule cimRegularIntervalSchedule, ResourceDescription rd)
        {
            if ((cimRegularIntervalSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateBasicIntervlScheduleProperties(cimRegularIntervalSchedule, rd);

                if (cimRegularIntervalSchedule.EndTimeHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_ENDTIME, cimRegularIntervalSchedule.EndTime));
                }
                if (cimRegularIntervalSchedule.TimeStepHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARINTERVALSCHEDULE_TIMESTAMP, cimRegularIntervalSchedule.TimeStep));
                }
            }

            // Ne pisem referencu 

        }

        //IRREGULAR TIME POINT
        public static void PopulateIrregularTimePointProperties(FTN.IrregularTimePoint cimIrregularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimIrregularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimIrregularTimePoint, rd);

                
                if (cimIrregularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE1, cimIrregularTimePoint.Value1));
                }
                if (cimIrregularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_VALUE2, cimIrregularTimePoint.Value2));
                }

                // referenca na IrregularIntervalSchedule
                if (cimIrregularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimIrregularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimIrregularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimIrregularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to PowerTransformer: rdfID \"").Append(cimIrregularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }
            }
        }

        //REGULAR TIME POINT
        public static void PopulateRegularTimePointProperties(FTN.RegularTimePoint cimRegularTimePoint, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimRegularTimePoint != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimRegularTimePoint, rd);

                
                if (cimRegularTimePoint.SequenceNumberHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_SEQUENENUMBER, cimRegularTimePoint.SequenceNumber));
                }
                if (cimRegularTimePoint.Value1HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE1, cimRegularTimePoint.Value1));
                }
                if (cimRegularTimePoint.Value2HasValue)
                {
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_VALUE2, cimRegularTimePoint.Value2));
                }

                // Referenca sa Regular Interval Schedule
                if (cimRegularTimePoint.IntervalScheduleHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimRegularTimePoint.IntervalSchedule.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimRegularTimePoint.GetType().ToString()).Append(" rdfID = \"").Append(cimRegularTimePoint.ID);
                        report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimRegularTimePoint.IntervalSchedule.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE, gid));
                }
            }
        }

        // OUTAGE SCHEDULE
        public static void PopulateOutageScheduleProperties(FTN.OutageSchedule cimOutageSchedule, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimOutageSchedule != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIrregularIntervalScheduleProperties(cimOutageSchedule, rd);

                // referenca na Power System Resource 
                if (cimOutageSchedule.PowerSystemResourceHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimOutageSchedule.PowerSystemResource.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimOutageSchedule.GetType().ToString()).Append(" rdfID = \"").Append(cimOutageSchedule.ID);
                        report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimOutageSchedule.PowerSystemResource.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE, gid));
                }
            }
        }

        // POWER SYSTEM RESOURCE
        public static void PopulatePowerSystemResourceProperties(FTN.PowerSystemResource cimPowerSystemResource, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            
            if ((cimPowerSystemResource != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimPowerSystemResource, rd);

            }

            // ne pisem za listu referenci!!!!!!!!!!
        }

        // EQUIPMENT
        public static void PopulateEquipmentProperties(FTN.Equipment cimEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulatePowerSystemResourceProperties(cimEquipment, rd, importHelper, report);

                if (cimEquipment.AggregateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_AGGREGATE, cimEquipment.Aggregate));
                }
                if (cimEquipment.NormallyInServiceHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.EQUIPMENT_NORMALLYINSERVICE, cimEquipment.NormallyInService));
                }
            }
        }

        // CURVE DATA
        public static void PopulateCurveDataProperties(FTN.CurveData cimCurveData, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimCurveData != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurveData, rd);


                if (cimCurveData.XvalueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_XVALUE, cimCurveData.Xvalue));
                }
                if (cimCurveData.Y1valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y1VALUE, cimCurveData.Y1value));
                }
                if (cimCurveData.Y2valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y2VALUE, cimCurveData.Y2value));
                }
                if (cimCurveData.Y3valueHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_Y3VALUE, cimCurveData.Y3value));
                }

                // referenca na Curve
                if (cimCurveData.CurveHasValue)
                {
                    long gid = importHelper.GetMappedGID(cimCurveData.Curve.ID);
                    if (gid < 0)
                    {
                        report.Report.Append("WARNING: Convert ").Append(cimCurveData.GetType().ToString()).Append(" rdfID = \"").Append(cimCurveData.ID);
                        report.Report.Append("\" - Failed to set reference to Location: rdfID \"").Append(cimCurveData.Curve.ID).AppendLine(" \" is not mapped to GID!");
                    }
                    rd.AddProperty(new Property(ModelCode.CURVEDATA_CURVE, gid));
                }

            }
        }

        // CURVE
        public static void PopulateCurveProperties(FTN.Curve cimCurve, ResourceDescription rd)
        {
            //NE STAVLJAM LISTU REFERENCI KOLIKO VIDIM
            if ((cimCurve != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateIdentifiedObjectProperties(cimCurve, rd);

                if (cimCurve.CurveStyleHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_CURVESTYLE, (short)GetDMSCurveStyle(cimCurve.CurveStyle)));
                }
                if (cimCurve.XMultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_XMULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.XMultiplier)));
                }
                if (cimCurve.XUnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_XUNIT, (short)GetDMSUnitSymbol(cimCurve.XUnit)));
                }
                if (cimCurve.Y1MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y1Multiplier)));
                }
                if (cimCurve.Y1UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y1UNIT, (short)GetDMSUnitSymbol(cimCurve.Y1Unit)));
                }
                if (cimCurve.Y2MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y2Multiplier)));
                }
                if (cimCurve.Y2UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y2UNIT, (short)GetDMSUnitSymbol(cimCurve.Y2Unit)));
                }
                if (cimCurve.Y3MultiplierHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3MULTIPLIER, (short)GetDMSUnitMultiplier(cimCurve.Y3Multiplier)));
                }
                if (cimCurve.Y3UnitHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.CURVE_Y3UNIT, (short)GetDMSUnitSymbol(cimCurve.Y3Unit)));
                }
            }

        }

        // CONDUCTING EQUIPMENT
        public static void PopulateConductingEquipmentProperties(FTN.ConductingEquipment cimConductingEquipment, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimConductingEquipment != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateEquipmentProperties(cimConductingEquipment, rd, importHelper, report);
            }
        }

        // SWITCH
        public static void PopulateSwitchProperties(FTN.Switch cimSwitch, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimSwitch != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateConductingEquipmentProperties(cimSwitch, rd, importHelper, report);

                if (cimSwitch.NormalOpenHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_NORMALOPEN, cimSwitch.NormalOpen));
                }
                if (cimSwitch.RatedCurrentHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RATEDCURRENT, cimSwitch.RatedCurrent));
                }
                if (cimSwitch.RetainedHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_RETAINED, cimSwitch.Retained));
                }
                if (cimSwitch.SwitchOnCountHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCONCOUNT, cimSwitch.SwitchOnCount));
                }
                if (cimSwitch.SwitchOnDateHasValue)
                {
                    rd.AddProperty(new Property(ModelCode.SWITCH_SWITCHONDATE, cimSwitch.SwitchOnDate));
                }
            }
        }

        // DISCONNECTOR
        public static void PopulateDisconnectorProperties(FTN.Disconnector cimDisconnector, ResourceDescription rd, ImportHelper importHelper, TransformAndLoadReport report)
        {
            if ((cimDisconnector != null) && (rd != null))
            {
                PowerTransformerConverter.PopulateSwitchProperties(cimDisconnector, rd, importHelper, report);
            }
        }
        #endregion 

        #region ENUMI
        public static CurveStyle GetDMSCurveStyle(FTN.CurveStyle curveStyle)
        {
            switch (curveStyle)
            {
                case FTN.CurveStyle.constantYValue:
                    return CurveStyle.constantYValue;
                case FTN.CurveStyle.formula:
                    return CurveStyle.formula;
                case FTN.CurveStyle.rampYValue:
                    return CurveStyle.rampYValue;
                case FTN.CurveStyle.straightLineYValues:
                    return CurveStyle.straightLineYValues;
                default: return CurveStyle.constantYValue;
            }
        }

        public static UnitMultiplier GetDMSUnitMultiplier(FTN.UnitMultiplier unitMultiplier)
        {
            switch (unitMultiplier)
            {
                case FTN.UnitMultiplier.c:
                    return UnitMultiplier.c;
                case FTN.UnitMultiplier.d:
                    return UnitMultiplier.d;
                case FTN.UnitMultiplier.G:
                    return UnitMultiplier.G;
                case FTN.UnitMultiplier.k:
                    return UnitMultiplier.k;
                case FTN.UnitMultiplier.m:
                    return UnitMultiplier.m;
                case FTN.UnitMultiplier.M:
                    return UnitMultiplier.M;
                case FTN.UnitMultiplier.micro:
                    return UnitMultiplier.micro;
                case FTN.UnitMultiplier.n:
                    return UnitMultiplier.n;
                case FTN.UnitMultiplier.none:
                    return UnitMultiplier.none;
                case FTN.UnitMultiplier.p:
                    return UnitMultiplier.p;
                case FTN.UnitMultiplier.T:
                    return UnitMultiplier.T;
                default: return UnitMultiplier.c;
            }
        }

        public static UnitSymbol GetDMSUnitSymbol(FTN.UnitSymbol unitSymbol)
        {
            switch (unitSymbol)
            {
                case FTN.UnitSymbol.A:
                    return UnitSymbol.A;
                case FTN.UnitSymbol.deg:
                    return UnitSymbol.deg;
                case FTN.UnitSymbol.degC:
                    return UnitSymbol.degC;
                case FTN.UnitSymbol.F:
                    return UnitSymbol.F;
                case FTN.UnitSymbol.g:
                    return UnitSymbol.g;
                case FTN.UnitSymbol.h:
                    return UnitSymbol.h;
                case FTN.UnitSymbol.H:
                    return UnitSymbol.H;
                case FTN.UnitSymbol.Hz:
                    return UnitSymbol.Hz;
                case FTN.UnitSymbol.J:
                    return UnitSymbol.J;
                case FTN.UnitSymbol.m:
                    return UnitSymbol.m;
                case FTN.UnitSymbol.m2:
                    return UnitSymbol.m2;
                case FTN.UnitSymbol.m3:
                    return UnitSymbol.m3;
                case FTN.UnitSymbol.min:
                    return UnitSymbol.min;
                case FTN.UnitSymbol.N:
                    return UnitSymbol.N;
                case FTN.UnitSymbol.none:
                    return UnitSymbol.none;
                case FTN.UnitSymbol.ohm:
                    return UnitSymbol.ohm;
                case FTN.UnitSymbol.Pa:
                    return UnitSymbol.Pa;
                case FTN.UnitSymbol.rad:
                    return UnitSymbol.rad;
                case FTN.UnitSymbol.s:
                    return UnitSymbol.s;
                case FTN.UnitSymbol.S:
                    return UnitSymbol.S;
                case FTN.UnitSymbol.V:
                    return UnitSymbol.V;
                case FTN.UnitSymbol.VA:
                    return UnitSymbol.VA;
                case FTN.UnitSymbol.VAh:
                    return UnitSymbol.VAh;
                case FTN.UnitSymbol.VAr:
                    return UnitSymbol.VAr;
                case FTN.UnitSymbol.VArh:
                    return UnitSymbol.VArh;
                case FTN.UnitSymbol.W:
                    return UnitSymbol.W;
                case FTN.UnitSymbol.Wh:
                    return UnitSymbol.Wh;
                default: return UnitSymbol.A;

            }
        }
        #endregion
    }
}
