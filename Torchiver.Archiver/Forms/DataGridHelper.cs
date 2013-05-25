using System.Linq;
using System.Windows.Forms;

namespace Torchiver.Archiver.Forms
{
    public static class DataGridHelper
    {
        public static object GetCellValueFromColumnHeader(this DataGridViewCellCollection cellCollection,
                                                          string headerText)
        {
            return cellCollection.Cast<DataGridViewCell>().First(c => c.OwningColumn.HeaderText == headerText).Value;
        }
    }
}