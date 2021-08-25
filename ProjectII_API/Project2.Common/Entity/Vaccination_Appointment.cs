using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Common.Entity
{
    /// <summary>
    /// Khởi tạo class Lịch hẹn tiêm
    /// </summary>
   public class Vaccination_Appointment
    {
        [Column] public Guid UserId { get; set; }
        /// <summary>
        /// Khóa chính
        /// Id lịch hẹn 
        /// </summary>
        [Column] public Guid Vaccination_appointmentId { get; set; }
        /// <summary>
        /// Ngày hẹn tiêm
        /// </summary>
        [Column] public DateTime Vaccination_appointment_date { get; set; }
        /// <summary>
        /// Loại vaccine sẽ tiêm
        /// </summary>
        [Column] public string Vaccine_name { get; set; }
        /// <summary>
        /// Tình trạng sức khỏe trước khi tiêm
        /// </summary>
        [Column] public string Health_condition { get; set; }

    }
}
