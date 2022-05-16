using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using FTN.Common;


namespace FTN.Services.NetworkModelService.DataModel.Core
{
	public class PowerSystemResource : IdentifiedObject
	{
		List<long> OutageSchedules = new List<long>();

		public PowerSystemResource(long globalId)
			: base(globalId)
		{
		}	

		public override bool Equals(object obj)
		{
			if (base.Equals(obj))
			{
				PowerSystemResource x = (PowerSystemResource)obj;
				return (CompareHelper.CompareLists(x.OutageSchedules, this.OutageSchedules)); // ima vec napravljena metoda
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

                case ModelCode.POWERSYSTEMRESOURCE_OUTAGESCHEDULES:
                    return true;

                default:
                    return base.HasProperty(t);

            }
        }

        public override void GetProperty(Property property)
        {
            switch (property.Id)
            {

                case ModelCode.POWERSYSTEMRESOURCE_OUTAGESCHEDULES:
                    property.SetValue(OutageSchedules);
                    break;

                default:
                    base.GetProperty(property);
                    break;
            }
        }

        #endregion IAccess implementation

        #region IReference implementation

        public override bool IsReferenced
        {
            get
            {
                return OutageSchedules.Count != 0 || base.IsReferenced;
            }
        }

        public override void GetReferences(Dictionary<ModelCode, List<long>> references, TypeOfReference refType)
        {
            if (OutageSchedules != null && OutageSchedules.Count != 0 && (refType == TypeOfReference.Target || refType == TypeOfReference.Both))
            {
                references[ModelCode.POWERSYSTEMRESOURCE_OUTAGESCHEDULES] = OutageSchedules.GetRange(0, OutageSchedules.Count);
            }

            base.GetReferences(references, refType);
        }

        public override void AddReference(ModelCode referenceId, long globalId)
        {
            switch (referenceId)
            {
                case ModelCode.OUTAGESCHEDULE_POWERSYSTEMRESOURCE:
                    OutageSchedules.Add(globalId);
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
                case ModelCode.POWERSYSTEMRESOURCE_OUTAGESCHEDULES:

                    if (OutageSchedules.Contains(globalId))
                    {
                        OutageSchedules.Remove(globalId);
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
