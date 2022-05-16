using FTN.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Core
{
    public class CurveData : IdentifiedObject
    {
        private float xvalue;
        private float y1value;
        private float y2value;
        private float y3value;
        private long curve = 0; // kod agregacije

        public CurveData(long globalId) : base(globalId)
        {

        }

        #region GET&SET
        public float Xvalue
        {
            get
            {
                return xvalue;
            }

            set
            {
                xvalue = value;
            }
        }

        public float Y1value
        {
            get
            {
                return y1value;
            }

            set
            {
                y1value = value;
            }
        }

        public float Y2value
        {
            get
            {
                return y2value;
            }

            set
            {
                y2value = value;
            }
        }

        public float Y3value
        {
            get
            {
                return y3value;
            }

            set
            {
                y3value = value;
            }
        }

        public long Curve
        {
            get
            {
                return curve;
            }

            set
            {
                curve = value;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                CurveData curve = (CurveData)obj;
                return (
                        curve.xvalue == this.xvalue &&
                        curve.y1value == this.y1value &&
                        curve.y2value == this.y2value &&
                        curve.y3value == this.y3value) &&
                        curve.curve == this.curve;
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

        #region IAccess implementation HAS PROPERTY i GET PROPERTY i SET PROPERTY
        public override bool HasProperty(ModelCode mc)
        {
            switch (mc)
            {            
                case ModelCode.CURVEDATA_XVALUE:
                case ModelCode.CURVEDATA_Y1VALUE:
                case ModelCode.CURVEDATA_Y2VALUE:
                case ModelCode.CURVEDATA_Y3VALUE:
                case ModelCode.CURVEDATA_CURVE:

                    return true;
                default:
                    return base.HasProperty(mc);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
               
                case ModelCode.CURVEDATA_XVALUE:
                    property.SetValue(xvalue);
                    break;

                case ModelCode.CURVEDATA_Y1VALUE:
                    property.SetValue(y1value);
                    break;

                case ModelCode.CURVEDATA_Y2VALUE:
                    property.SetValue(y2value);
                    break;

                case ModelCode.CURVEDATA_Y3VALUE:
                    property.SetValue(y3value);
                    break;

                case ModelCode.CURVEDATA_CURVE:
                    property.SetValue(curve);
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
                case ModelCode.CURVEDATA_XVALUE:
                    xvalue = property.AsFloat();
                    break;

                case ModelCode.CURVEDATA_Y1VALUE:
                    y1value = property.AsFloat();
                    break;

                case ModelCode.CURVEDATA_Y2VALUE:
                    y2value = property.AsFloat();
                    break;

                case ModelCode.CURVEDATA_Y3VALUE:
                    y3value = property.AsFloat();
                    break;

                case ModelCode.CURVEDATA_CURVE:
                    curve = property.AsReference();
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
            if (curve != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.CURVEDATA_CURVE] = new List<long>();
                references[ModelCode.CURVEDATA_CURVE].Add(curve);
            }

            base.GetReferences(references, refType);
        }


        #endregion IReference implementation



    }
}
