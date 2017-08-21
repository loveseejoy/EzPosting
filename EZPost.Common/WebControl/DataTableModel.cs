using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZPost.Common.WebControl
{
    public class DataTableModel<TEntity>
    {
        public DataTableModel(int drawParam, int recordsTotalParam, int recordsFilteredParam, IReadOnlyList<TEntity> dataParam)
        {
            draw = drawParam;
            recordsTotal = recordsTotalParam;
            recordsFiltered = recordsFilteredParam;
            data = dataParam;
        }
        public DataTableModel(string errorParam)
        {
            error = errorParam;
        }
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public IReadOnlyList<TEntity> data { get; set; }
        public string error { get; set; }
    }
}
