using System;
using System.Collections.Generic;

namespace Model_New.Models;

public partial class ShopDisplaySurvey
{
    public int SurveyId { get; set; }

    public int UserId { get; set; }

    public string? Empno { get; set; }

    public string? EmpEmail { get; set; }

    public DateTime? SurveyDate { get; set; }

    public bool? IsDisplayVisible { get; set; }

    public string? DisplayOption { get; set; }

    public byte[]? DisplayPhotoBefore { get; set; }

    public byte[]? DisplayPhotoAfter { get; set; }

    public bool? AreUnileverBrandsSeparate { get; set; }

    public bool? AreBrandVariantsSeparate { get; set; }

    public bool? AreNonUnileverBrandsBetween { get; set; }

    public bool? IsUnileverShelfStripAvailable { get; set; }

    public bool? IsArrangementAdequateBefore { get; set; }

    public bool? MissingItemsBefore { get; set; }

    public bool? NewProductsAddedAfter { get; set; }

    public bool? RearrangementDoneAfter { get; set; }

    public bool? EnhancedVisibilityAfter { get; set; }

    public int Rscid { get; set; }

    public int? Rscode { get; set; }

    public string? RsName { get; set; }

    public string? PartyMasterCode { get; set; }

    public string? PartyHllcode { get; set; }

    public string? PartyName { get; set; }

    public virtual OutLetMasterDetail Rsc { get; set; } = null!;

    public virtual TblUser User { get; set; } = null!;
}
