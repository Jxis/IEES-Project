using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Package_IES_Project120
{
    public class BasicIntervalSchedule : IdentifiedObject
    {
        private DateTime startTime;
        private UnitMultiplier value1Multiplier;
        private UnitSymbol value1Symbol;
        private UnitMultiplier value2Multiplier;
        private UnitSymbol value2Symbol;

        public BasicIntervalSchedule(long globalId) : base(globalId)
        {
        }

        #region GET&SET
        public DateTime StartTime
        {
            get
            {
                return startTime;
            }

            set
            {
                startTime = value;
            }
        }

        public UnitMultiplier Value1Multiplier
        {
            get
            {
                return value1Multiplier;
            }

            set
            {
                value1Multiplier = value;
            }
        }

        public UnitSymbol Value1Symbol
        {
            get
            {
                return value1Symbol;
            }

            set
            {
                value1Symbol = value;
            }
        }

        public UnitMultiplier Value2Multiplier
        {
            get
            {
                return value2Multiplier;
            }

            set
            {
                value2Multiplier = value;
            }
        }

        public UnitSymbol Value2Symbol
        {
            get
            {
                return value2Symbol;
            }

            set
            {
                value2Symbol = value;
            }
        }

        #endregion

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                BasicIntervalSchedule x = (BasicIntervalSchedule)obj;
                return (x.startTime == this.startTime &&
                        x.value1Multiplier == this.value1Multiplier &&
                        x.value1Symbol == this.value1Symbol &&
                        x.value2Multiplier == this.value2Multiplier &&
                        x.value2Symbol == this.value2Symbol);
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
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:

                    return true;
                default:
                    return base.HasProperty(t);
            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
               
                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                    property.SetValue((short)value1Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                    property.SetValue((short)value1Symbol);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                    property.SetValue((short)value2Multiplier);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    property.SetValue((short)value2Symbol);
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    property.SetValue(startTime);
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

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1MULTIPLIER:
                    value1Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE1UNIT:
                    value1Symbol = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2MULTIPLIER:
                    value2Multiplier = (UnitMultiplier)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_VALUE2UNIT:
                    value2Symbol = (UnitSymbol)property.AsEnum();
                    break;

                case ModelCode.BASICINTERVALSCHEDULE_STARTTIME:
                    startTime = property.AsDateTime();
                    break;

                default:
                    base.SetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation



    }
}

