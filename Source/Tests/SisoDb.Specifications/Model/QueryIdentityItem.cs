using SisoDb.NCore;

namespace SisoDb.Specifications.Model
{
    public class QueryIdentityItem : QueryXItem<int>
    {
        public override string AsJson()
        {
            return JsonFormat.Inject(StructureId, SortOrder, IntegerValue, NullableIntegerValue, StringValue, GuidValue.ToString("N"), BoolValue.ToString().ToLower());
        }
    }
}