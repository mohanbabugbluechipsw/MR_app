using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_New.Models
{
    public class Tbl_capturedata_log
    {
        public int Id { get; set; }
        public string Rscode { get; set; }
        public string MrCode { get; set; }
        public string Outlet { get; set; }
        public string OutletType { get; set; }
        public byte[] CapturedPhoto { get; set; } // ✅ Store Image as Byte Array

        public string? SrCode { get; set; }
        public string? SrCodeName { get; set; }
        public DateTime UploadedAt { get; set; }


    }
}
