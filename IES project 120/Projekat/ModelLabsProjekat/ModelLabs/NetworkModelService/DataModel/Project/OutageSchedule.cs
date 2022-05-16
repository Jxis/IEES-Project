using FTN.Common;
using FTN.Services.NetworkModelService.DataModel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FTN.Services.NetworkModelService.DataModel.Package_IES_Project120
{
    public class OutageSchedule : IrregularIntervalSchedule
    {
        private long powerSystemResource = 0;

        public OutageSchedule(long globalId) : base(globalId)
        {

        }

        public long PowerSystemResource
        {
            get
            {
                return powerSystemResource;
            }

            set
            {
                powerSystemResource = value;
            }
        }

        public override bool Equals(object obj)
        {
            if (base.Equals(obj))
            {
                OutageSchedule x = (OutageSchedule)obj;
                return (x.powerSystemResource == this.powerSystemResource);
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
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    return true;

                default:
                    return base.HasProperty(cd);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    property.SetValue(PowerSystemResource);
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
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    PowerSystemResource = property.AsReference();
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
            if (PowerSystemResource != 0 && (refType == TypeOfReference.Reference || refType == TypeOfReference.Both))
            {
                references[ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE] = new List<long>();
                references[ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE].Add(PowerSystemResource);
            }

            base.GetReferences(references, refType);
        }


        #endregion IReference implementation
    }
}
