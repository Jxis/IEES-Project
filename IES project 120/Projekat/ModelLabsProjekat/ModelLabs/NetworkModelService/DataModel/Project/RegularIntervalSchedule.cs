using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Package_IES_Project120
{
    public class RegularIntervalSchedule : BasicIntervalSchedule
    {
        private DateTime endTime;
        private float timeStamp;
        private List<long> timePoints = new List<long>(); // sa regulartimepoint

        public RegularIntervalSchedule(long globalId) : base(globalId)
        {
        }

        #region GET&SET

        public DateTime EndTime
        {
            get
            {
                return endTime;
            }

            set
            {
                endTime = value;
            }
        }

        public float TimeStamp
        {
            get
            {
                return timeStamp;
            }

            set
            {
                timeStamp = value;
            }
        }

        public List<long> TimePoints
        {
            get
            {
                return timePoints;
            }

            set
            {
                timePoints = value;
            }
        }
#endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularIntervalSchedule x = (RegularIntervalSchedule)obj;
                return (x.endTime == this.endTime && x.timeStamp == this.timeStamp && CompareHelper.CompareLists(x.TimePoints, this.TimePoints));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #region IAccess implementation

        public override bool HasProperty(ModelCode cd)
        {
            switch (cd)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTAMP:
                case ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS:
                    return true;

                default:
                    return base.HasProperty(cd);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    property.SetValue(endTime);
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTAMP:
                    property.SetValue(timeStamp);
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS:
                    property.SetValue(TimePoints);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        public override void SetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_ENDTIME:
                    endTime = property.AsDateTime();
                    break;

                case ModelCode.REGULARINTERVALSCHEDULE_TIMESTAMP:
                    timeStamp = property.AsFloat();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return TimePoints.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (TimePoints != null && TimePoints.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS] = TimePoints.GetRange(0, TimePoints.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    TimePoints.Add(globalId);
                    break;

                default:
                    base.AddReference(referenceId, globalId);
                    break;
            }
        }

        public override void RemoveReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.REGULARINTERVALSCHEDULE_TIMEPOINTS:

                    if (TimePoints.Contains(globalId))
                    {
                        TimePoints.Remove(globalId);
                    }
                    else
                    {
                        CommonTrace.WriteTrace(CommonTrace.TraceWarning, "Entity (GID = 0x{0:x16}) doesn't contain reference 0x{1:x16}.", this.GlobalId, globalId);
                    }

                    break;

                default:
                    base.RemoveReference(referenceId, globalId);
                    break;
            }
        }

        #endregion IReference implementation
    }
}
