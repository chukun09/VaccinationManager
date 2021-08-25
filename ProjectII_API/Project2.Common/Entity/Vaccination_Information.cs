using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Common.Entity
{
    /// <summary>
    /// Tạo class Thông tin đã tiêm chủng
    /// Thông tin Vaccination_Information
    /// </summary>
   public class Vaccination_Information
    {
        [Column] public Guid UserId { get; set; }
        /// <summary>
        /// Ngày tiêm 
        /// </summary>
        [Column] public DateTime Vaccination_date { get; set; }
        /// <summary>
        /// Tên loại Vaccine tiêm
        /// </summary>
        [Column] public string Vaccine_name { get; set; }
        /// <summary>
        /// Tình trạng sức khỏe sau khi tiêm
        /// </summary>
        [Column] public string Health_condition { get; set; }
        /// <summary>
        /// Khóa chính
        /// Id của thông tin tiêm chủng
        /// </summary>
        [Column] public Guid Vaccination_InformationId { get; set; }
    }
}
