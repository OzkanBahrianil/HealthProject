using EntityLayer.Concrate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
  public  interface IBlogService: IGenericService<Blog>
    {

        List<Blog> GetBlogListById(int id);
        List<Blog> GetListWriterBlog(int id);
        List<Blog> GetBlogListWithCategory();
        List<Blog> GetBlogListWithCategoryWithComments();
      
    }
}
