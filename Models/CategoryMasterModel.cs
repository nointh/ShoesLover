using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesLover.Models
{
    public class CategoryMasterModel : Category
    {
        public List<SubCategory> SubCategoryList;
    }
}
