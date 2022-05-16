using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class RegularTimePoint : IdentifiedObject
    {
        private int sequenceNumber;
        private float value1;
        private float value2;
        private long intervalSchedule = 0; // prema regularIntervalSchedule

        public RegularTimePoint(long globalId) : base(globalId)
        {

        }

        #region GET&SET
        public int SequenceNumber
        {
            get
            {
                return sequenceNumber;
            }

            set
            {
                sequenceNumber = value;
            }
        }

        public float Value1
        {
            get
            {
                return value1;
            }

            set
            {
                value1 = value;
            }
        }

        public float Value2
        {
            get
            {
                return value2;
            }

            set
            {
                value2 = value;
            }
        }

        public long IntervalSchedule
        {
            get
            {
                return intervalSchedule;
            }

            set
            {
                intervalSchedule = value;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                RegularTimePoint x = (RegularTimePoint)obj;
                return (x.sequenceNumber == this.sequenceNumber &&
                        x.value1 == this.value1 &&
                        x.value2 == this.value2 &&
                        x.intervalSchedule == this.intervalSchedule);
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

        public override bool HasProperty(ModelCode mc)
        {
            switch (mc)
            {
                
                case ModelCode.REGULARTIMEPOINT_SEQUENENUMBER:
                case ModelCode.REGULARTIMEPOINT_VALUE1:
                case ModelCode.REGULARTIMEPOINT_VALUE2:
                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:

                    return true;
                default:
                    return base.HasProperty(mc);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                
                case ModelCode.REGULARTIMEPOINT_SEQUENENUMBER:
                    property.SetValue(sequenceNumber);
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE2:
                    property.SetValue(value2);
                    break;

                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    property.SetValue(IntervalSchedule);
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
                case ModelCode.REGULARTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.REGULARTIMEPOINT_SEQUENENUMBER:
                    sequenceNumber = property.AsInt();
                    break;

                case ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE:
                    IntervalSchedule = property.AsReference();
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
                return base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (IntervalSchedule != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE] = new List<long>();
                references[ModelCode.REGULARTIMEPOINT_INTERVALSCHEDULE].Add(IntervalSchedule);
            }

            base.GetReferences(references, refType);
        }


        #endregion IReference implementation
    }

}
