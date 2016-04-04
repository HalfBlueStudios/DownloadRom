using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadRom
{
    class optionList
    {
        List<selectionOption> selectionList;
        
        public optionList(List<selectionOption> newListOfOptions)
        {
            selectionList = newListOfOptions;
        }

        public List<selectionOption> getSelectionList()
        {
            return (selectionList);
        }
    }
}
