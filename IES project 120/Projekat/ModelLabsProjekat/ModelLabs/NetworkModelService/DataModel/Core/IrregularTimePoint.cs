using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class IrregularTimePoint : IdentifiedObject
    {
        private float time;
        private float value1;
        private float value2;
        private long intervalSchedule = 0; // agregacija sa regularintervalschedule

        public IrregularTimePoint(long globalId) : base(globalId)
        {

        }

        #region GET&SET
        public float Time
        {
            get
            {
                return time;
            }

            set
            {
                time = value;
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
                IrregularTimePoint irTimePoint = (IrregularTimePoint)obj;
                return (irTimePoint.time == this.time &&
                        irTimePoint.value1 == this.value1 &&
                        irTimePoint.value2 == this.value2 &&
                        irTimePoint.IntervalSchedule == this.IntervalSchedule);
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

        public override bool HasProperty(ModelCode t)
        {
            switch (t)
            {
                
                case ModelCode.IRREGULARTIMEPOINT_TIME:
                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                case ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE:

                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                
                case ModelCode.IRREGULARTIMEPOINT_TIME:
                    property.SetValue(time);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    property.SetValue(value1);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    property.SetValue(value2);
                    break;

                case ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE:
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
                case ModelCode.IRREGULARTIMEPOINT_VALUE2:
                    value2 = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_VALUE1:
                    value1 = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_TIME:
                    time = property.AsFloat();
                    break;

                case ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE:
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
                references[ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE] = new List<long>();
                references[ModelCode.IRREGULARTIMEPOINT_INTERVALSCHEDULE].Add(IntervalSchedule);
            }

            base.GetReferences(references, refType);
        }


        #endregion IReference implementation
    }
}

