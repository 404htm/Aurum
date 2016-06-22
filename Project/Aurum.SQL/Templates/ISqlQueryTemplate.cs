namespace Aurum.SQL.Templates
{

	using Filter = System.Func<Aurum.SQL.Data.SqlTableDetail, bool>;
	using Query = System.Func<Aurum.SQL.Data.SqlTableDetail, string>;

	public interface ISqlQueryTemplate
	{
		bool AllowAutoSubquery { get; set; }
		string Description { get; set; }
		string FilterText { get; set; }
		bool IsDestructive { get; set; }
		string Name { get; set; }
		string QueryName { get; set; }
		string QueryText { get; set; }

		Filter AppliesTo { get; }
		Query GetQuery { get; }

	}
}