using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Package_IES_Project120;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Wires
{
    public class Switch : ConductingEquipment
    {
        private bool normalOpen;
        private float ratedCurrent;
        private bool retained;
        private int switchOnCount;
        private DateTime switchOnDate;

        public Switch(long globalId) : base(globalId)
        {
        }

        #region GET&SET
        public bool NormalOpen
        {
            get
            {
                return normalOpen;
            }

            set
            {
                normalOpen = value;
            }
        }

        public float RatedCurrent
        {
            get
            {
                return ratedCurrent;
            }

            set
            {
                ratedCurrent = value;
            }
        }

        public bool Retained
        {
            get
            {
                return retained;
            }

            set
            {
                retained = value;
            }
        }

        public int SwitchOnCount
        {
            get
            {
                return switchOnCount;
            }

            set
            {
                switchOnCount = value;
            }
        }


        public DateTime SwitchOnDate
        {
            get
            {
                return switchOnDate;
            }

            set
            {
                switchOnDate = value;
            }
        }
        #endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                Switch x = (Switch)obj;
                return (x.normalOpen == this.normalOpen &&
                        x.ratedCurrent == this.ratedCurrent &&
                        x.retained == this.retained &&
                        x.switchOnCount == this.switchOnCount &&
                        x.switchOnDate == this.switchOnDate);
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
                case ModelCode.SWITCH_NORMALOPEN:
                case ModelCode.SWITCH_RATEDCURRENT:
                case ModelCode.SWITCH_RETAINED:
                case ModelCode.SWITCH_SWITCONCOUNT:
                case ModelCode.SWITCH_SWITCHONDATE:

                    return true;

                default:
                    return base.HasProperty(mc);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.SWITCH_NORMALOPEN:
                    property.SetValue(normalOpen);
                    break;

                case ModelCode.SWITCH_RATEDCURRENT:
                    property.SetValue(ratedCurrent);
                    break;

                case ModelCode.SWITCH_RETAINED:
                    property.SetValue(retained);
                    break;

                case ModelCode.SWITCH_SWITCONCOUNT:
                    property.SetValue(switchOnCount);
                    break;

                case ModelCode.SWITCH_SWITCHONDATE:
                    property.SetValue(switchOnDate);
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

                case ModelCode.SWITCH_NORMALOPEN:
                    normalOpen = property.AsBool();
                    break;

                case ModelCode.SWITCH_RATEDCURRENT:
                    ratedCurrent = property.AsFloat();
                    break;

                case ModelCode.SWITCH_RETAINED:
                    retained = property.AsBool();
                    break;

                case ModelCode.SWITCH_SWITCONCOUNT:
                    switchOnCount = property.AsInt();
                    break;

                case ModelCode.SWITCH_SWITCHONDATE:
                    switchOnDate = property.AsDateTime();
                    break;


                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

    }
}
