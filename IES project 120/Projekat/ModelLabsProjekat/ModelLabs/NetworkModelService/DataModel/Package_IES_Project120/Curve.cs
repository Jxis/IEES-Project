using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Package_IES_Project120
{
    public class Curve : IdentifiedObject
    {
        private CurveStyle curveStyle;
        private UnitMultiplier xMultiplier;
        private UnitSymbol xUnit;
        private UnitMultiplier y1Multiplier;
        private UnitSymbol y1Unit;
        private UnitMultiplier y2Multiplier;
        private UnitSymbol y2Unit;
        private UnitMultiplier y3Multiplier;
        private UnitSymbol y3Unit;
        private List<long> CurveDatas = new List<long>(); // za curvedata

        #region GET&SET
        public CurveStyle CurveStyle { get => curveStyle; set => curveStyle = value; }
        public UnitMultiplier XMultiplier { get => xMultiplier; set => xMultiplier = value; }
        public UnitSymbol XUnit { get => xUnit; set => xUnit = value; }
        public UnitMultiplier Y1Multiplier { get => y1Multiplier; set => y1Multiplier = value; }
        public UnitSymbol Y1Unit { get => y1Unit; set => y1Unit = value; }
        public UnitMultiplier Y2Multiplier { get => y2Multiplier; set => y2Multiplier = value; }
        public UnitSymbol Y2Unit { get => y2Unit; set => y2Unit = value; }
        public UnitMultiplier Y3Multiplier { get => y3Multiplier; set => y3Multiplier = value; }
        public UnitSymbol Y3Unit { get => y3Unit; set => y3Unit = value; }
        public List<long> CurveDatas1 { get => CurveDatas; set => CurveDatas = value; }
        #endregion

        public Curve(long globalId) : base(globalId)
        {
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Curve x = (Curve)obj;
                return (x.curveStyle == this.curveStyle && x.xMultiplier == this.xMultiplier && x.xUnit == this.xUnit &&
                        x.y1Multiplier == this.y1Multiplier && x.y1Unit == this.y1Unit && x.y2Multiplier == this.y2Multiplier &&
                        x.y2Unit == this.y2Unit && x.y3Multiplier == this.y3Multiplier && x.y3Unit == this.y3Unit &&
                        CompareHelper.CompareLists(x.CurveDatas, this.CurveDatas));
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
                case ModelCode.CURVE_CURVESTYLE:
                case ModelCode.CURVE_XMULTIPLIER:
                case ModelCode.CURVE_XUNIT:
                case ModelCode.CURVE_Y1MULTIPLIER:
                case ModelCode.CURVE_Y1UNIT:
                case ModelCode.CURVE_Y2MULTIPLIER:
                case ModelCode.CURVE_Y2UNIT:
                case ModelCode.CURVE_Y3MULTIPLIER:
                case ModelCode.CURVE_Y3UNIT:
                case ModelCode.CURVE_CURVEDATAS:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {             

                case ModelCode.CURVE_XMULTIPLIER:
                    property.SetValue((short)xMultiplier);
                    break;

                case ModelCode.CURVE_XUNIT:
                    property.SetValue((short)xUnit);
                    break;

                case ModelCode.CURVE_Y1MULTIPLIER:
                    property.SetValue((short)y1Multiplier);
                    break;

                case ModelCode.CURVE_Y1UNIT:
                    property.SetValue((short)y1Unit);
                    break;

                case ModelCode.CURVE_Y2MULTIPLIER:
                    property.SetValue((short)y2Multiplier);
                    break;

                case ModelCode.CURVE_Y2UNIT:
                    property.SetValue((short)y2Unit);
                    break;

                case ModelCode.CURVE_Y3MULTIPLIER:
                    property.SetValue((short)y3Multiplier);
                    break;

                case ModelCode.CURVE_Y3UNIT:
                    property.SetValue((short)y3Unit);
                    break;

                case ModelCode.CURVE_CURVEDATAS:
                    property.SetValue(CurveDatas);
                    break;

                case ModelCode.CURVE_CURVESTYLE:
                    property.SetValue((short)curveStyle);
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
                case ModelCode.CURVE_XMULTIPLIER:
                    xMultiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.CURVE_XUNIT:
                    xUnit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.CURVE_Y1MULTIPLIER:
                    y1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.CURVE_Y1UNIT:
                    Y1Unit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.CURVE_Y2MULTIPLIER:
                    y2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.CURVE_Y2UNIT:
                    y2Unit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.CURVE_Y3MULTIPLIER:
                    y3Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.CURVE_Y3UNIT:
                    y3Unit = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.CURVE_CURVESTYLE:
                    curveStyle = (CurveStyle)property.AsEnum();
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
                return CurveDatas.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {

            if (CurveDatas != null && CurveDatas.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.CURVE_CURVEDATAS] = CurveDatas.GetRange(0, CurveDatas.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.CURVEDATA_CURVE:
                    CurveDatas.Add(globalId);
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
                case ModelCode.CURVE_CURVEDATAS:

                    if (CurveDatas.Contains(globalId))
                    {
                        CurveDatas.Remove(globalId);
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
