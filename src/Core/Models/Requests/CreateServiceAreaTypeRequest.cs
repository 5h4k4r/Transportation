namespace Core.Models.Requests;

public class CreateServiceAreaTypeRequest
{
    public ulong? Icon { get; set; }
    public string AreaId { get; set; }
    public ulong CategoryId { get; set; }
    public ulong BaseTypeId { get; set; }
    public ulong PersonTypeId { get; set; }
    public double BasePrice { get; set; }
    public double BaseTime { get; set; }
    public double BaseStop { get; set; }
    public double BaseStopDistance { get; set; }
    public double BaseDistance { get; set; }
    public double MinPrice { get; set; }
    public double Tip { get; set; }
    public double MinTip { get; set; }
    public double MaxTip { get; set; }
    public ulong UsageId { get; set; }
    public double BaseNight { get; set; }
    public double BaseNightStart { get; set; }
    public double BaseNightEnd { get; set; }
}

public class ServiceAreaTypeParams
{
    public double BasePrice { get; set; }
    public double BaseTime { get; set; }
    public double BaseStop { get; set; }
    public double BaseStopTime { get; set; }
    public double BaseDistance { get; set; }
    public double MinPrice { get; set; }
    public double BaseNight { get; set; }
    public ICollection<BaseNightPeriods> BaseNightPeriods { get; set; } = null!;
    public double Tip { get; set; }
    public double MinTip { get; set; }
    public double MaxTip { get; set; }
}

public class BaseNightPeriods
{
    public double BaseNightStart { get; set; }
    public double BaseNightEnd { get; set; }
}
//
// "icon": "",
// "area_id": "",
// "service_id": "2",
// "category_id": "",
// "shipping_id": "",
// "base_type_id": "",
// "person_type_id": "",
// "base_price": "",
// "base_time": "",
// "base_stop": "",
// "base_stop_time": "",
// "base_distance": "",
// "min_price": "",
// "tip": "",
// "min_tip": "",
// "max_tip": "",
// "keys": [],
// "labels": [],
// "base_night": "",
// "base_night_start": "",
// "base_night_end": ""