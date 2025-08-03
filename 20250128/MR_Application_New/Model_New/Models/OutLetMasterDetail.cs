using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class OutLetMasterDetail
{
    public int Id { get; set; }

    public int? Rscode { get; set; }

    public string? RsName { get; set; }

    public string? PartyMasterCode { get; set; }

    public string? PartyHllcode { get; set; }

    public string? PartyName { get; set; }

    public string? PrimaryChannel { get; set; }

    public string? SecondaryChannel { get; set; }

    public string? Category { get; set; }

    public string? ParStatus { get; set; }

    public TimeOnly? UpdateStamp { get; set; }

    public DateTime? OlCreatedDate { get; set; }

    public string? Address1 { get; set; }

    public string? Address2 { get; set; }

    public string? Address3 { get; set; }

    public string? Address4 { get; set; }

    public decimal? Latitude { get; set; }

    public decimal? Longitude { get; set; }

    public string? PrimarychannelCode { get; set; }

    public string? SecondarychannelCode { get; set; }

    public virtual ICollection<ShopDisplaySurvey> ShopDisplaySurveys { get; set; } = new List<ShopDisplaySurvey>();
}
