using System;

namespace EZPost.Dto
{
    public interface IDateDto
    {
         DateTime StartDate { set; get; }
        DateTime EndDate { set; get; }
    }
}