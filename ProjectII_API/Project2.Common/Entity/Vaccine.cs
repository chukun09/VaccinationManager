using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Common.Entity
{
    /// <summary>
    /// Tạo class Vacxin
    /// Thông tin Vaccine
    /// </summary>
    public class Vaccine
    {
        #region Property Vắc xin
        /// <summary>
        /// Tên Vacxin 
        /// Khóa chính
        /// </summary>
        public Guid VaccineId { get; set; }
        public string Vaccine_Name { get; set; }
        /// <summary>
        /// Số lượng Vacxin
        /// </summary>
        public int? Vaccine_Amount { get; set; }
        /// <summary>
        /// Xuất xứ
        /// </summary>
        public string Vaccine_Origin { get; set; }
        /// <summary>
        /// Ngày sản xuất Vacxin
        /// </summary>
        public DateTime? Date_Of_Manufacture { get; set; }
        #endregion
    }
}
