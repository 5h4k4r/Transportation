using Core.Interfaces;
using Core.Models.Common;

namespace Api.Helpers;

public class Helpers
{
    private const string BASE_CURRENCY = "setting.base_currency";

    //
    // public static double currencySymbol(currency)
    // {
    //     return config('setting.currency_symbol.'.currency);
    // }
    //
    // public static double currencyBase(amount, currency)
    // {
    //     currency_base = config(self::BASE_CURRENCY. '.'.currency);
    //     return amount * currency_base;
    // }
    //
    // public static double exchange(amount, currency)
    // {
    //     currency_base = config(self::BASE_CURRENCY. '.'.currency);
    //     if (amount > 0)
    //         return (int)(amount / currency_base);
    //     return amount;
    // }
    //
    // public static double base64(data)
    // {
    //     return rtrim(strtr(base64_encode(json_encode(data)), '+/', '-_'), '=');
    // }
    //
    public static async Task<double> RoundPrice(double amount, Currency currency, string? method,
        IUnitOfWork unitOfWork, bool exchange = false)
    {
        var rounder = await Rounder.Get(currency, unitOfWork);
        if (!rounder)
            return intval(amount);
        if (exchange)
            rounderdivisor *= config(self::BASE_CURRENCY. '.'.currency);
        mod = amount % rounderdivisor;

        out_side = intval(amount / rounderdivisor);
        sub_amount = out_side * rounderdivisor;

        if (method != '') roundermethod = method;

        if (strcasecmp(roundermethod, 'half') == 0)
        {
            if (mod > rounderdivisor / 2)
                amount = sub_amount + rounderdivisor;
            else
                amount = sub_amount;
        }

        else if (strcasecmp(roundermethod, 'toUp') == 0)
        {
            amount = sub_amount + rounderdivisor;
        }
        else if (strcasecmp(roundermethod, 'toDown') == 0)
        {
            amount = sub_amount;
        }

        return amount;
    }

    //
    // public static double plaque(plaque)
    // {
    //     if (is_null(plaque))
    //         return null;
    //
    //     plaque = json_decode(plaque);
    //
    //     if (plaquecountry == 'iran')
    //     {
    //         codes = str_split(plaquecode, 2);
    //
    //         if (!(new self)is_rtl(codes[1]))
    //         return json_encode(plaque);
    //
    //         code = @codes[2].@codes[3].@codes[1].@codes[0];
    //         plaquecode = code;
    //         plaque = json_encode(plaque);
    //     }
    //     else
    //     {
    //         plaque = json_encode(plaque);
    //     }
    //
    //     return plaque;
    // }
    //
    // private double is_rtl(string)
    // {
    //     rtl_chars_pattern = '/[\x{0590}-\x{05ff}\x{0600}-\x{06ff}]/u';
    //     return preg_match(rtl_chars_pattern, string);
    // }
    //
    // public static double checkAnyRequestFilled(request, except = [])
    // {
    //     data = array_keys(requestexcept(except));
    //     response = FALSE;
    //     foreach (data as key) {
    //         if (filled(requestkey))
    //         {
    //             response = TRUE;
    //             break;
    //         }
    //     }
    //     return response;
    // }
    //
    // public static double extractLatAndLng(location): array
    // {
    //     [lat, lng] =
    //     explode(',', location);
    //
    //     return [
    //     'lat' => (double)lat, 'lng' => (double)lng];
    // }
    //
    // public static double bankIdValidation(bank_id)
    // {
    //     bankCode = array(
    //         'A' => 10, 'G' => 16, 'M' => 22, 'S' => 28, 'Y' => 34,
    //     'B' => 11, 'H' => 17, 'N' => 23, 'T' => 29, 'Z' => 35,
    //     'C' => 12, 'I' => 18, 'O' => 24, 'U' => 30,
    //     'D' => 13, 'J' => 19, 'P' => 25, 'V' => 31,
    //     'E' => 14, 'K' => 20, 'Q' => 26, 'W' => 32,
    //     'F' => 15, 'L' => 21, 'R' => 27, 'X' => 33,
    //         );
    //     bank_code = '';
    //     bankId = "0";
    //
    //     for (i = 0; i < strlen(bank_id); i++)
    //     {
    //         if (i < 2 && array_key_exists(strtoupper(bank_id[i]), bankCode))
    //         {
    //             bank_code.= bankCode[strtoupper(bank_id[i])];
    //         }
    //
    //         elseif(i > 1 && i < 4) {
    //             bank_code.= bank_id[i];
    //         } else {
    //             bankId.= strval(bank_id[i]);
    //         }
    //     }
    //
    //     IBAN = str_replace(' ', '', bankId. ''.bank_code);
    //     if (preg_match('/^[0-9]{28,29}/', IBAN))
    //     {
    //         q = DB::query()select(DB::raw(' mod('.IBAN. ',97) as iban_mod'))first();
    //
    //         if (qiban_mod == 1)
    //             return TRUE;
    //         else
    //             return FALSE;
    //     }
    //     else
    //         return FALSE;
    // }
    //
    public static double percentage(double amount, double percent)
    {
        return amount * percent;
    }
    //
    // public static double stringPoints(points)
    // {
    //     string =  []
    //     ;
    //     foreach (points as point) {
    //         string[] = point['lng']. ','.point['lat'];
    //     }
    //
    //     return implode(';', string);
    // }
    //
    // public static double clearMobile(mobile)
    // {
    //     if (strcasecmp(substr(mobile, 0, 1), "+") != 0)
    //     {
    //         return "+".mobile;
    //     }
    //
    //     return mobile;
    // }
    //
    // public static double paginate()
    // {
    //     request = app('Illuminate\Http\Request');
    //     page = requestpage ?? 1;
    //
    //     if (requestpage and requestpage == 0)
    //     page = 1;
    //
    //     count = requestcount ?? 8;
    //
    //     offset = (page - 1) * count;
    //     if (page == 1 or page == 0)
    //     offset = 0;
    //
    //     return array('page' => page, 'count' => count, 'offset' => offset);
    // }
    //
    // public static double paginate_response(paging, total)
    // {
    //     return [
    //     'page' => (int)paging['page'],
    //     'nextPage' => paging['page'] + 1,
    //     'itemsPerPage' => (int)paging['count'],
    //     'totalItems' => total
    //         ];
    // }
    //
    // public static double setTrackId()
    // {
    //     unique_id = uniqid();
    //     RedisManager::setKey('tracking_id'.Auth::id(), unique_id, 1800);
    //     return unique_id;
    // }
    //
    // public static double getTrackId()
    // {
    //     return RedisManager::getKey('tracking_id'.Auth::id());
    // }
    //
    // public static double servantChannelObject(online_servants)
    // {
    //     data =  []
    //     ;
    //     foreach (online_servants as online_servant) {
    //         location = RedisManager::getPosition('servants_location'.online_servant['service']['id'],
    //             online_servant['user']['id']);
    //         array_push(data, [
    //             'user' => [
    //         'id' => online_servant['user']['id'],
    //         'mobile' => online_servant['user']['mobile'],
    //         'first_name' => @online_servant['user']['servant']['first_name'],
    //         'last_name' => @online_servant['user']['servant']['last_name'],
    //             ],
    //         'service' => [
    //         'id' => online_servant['service']['id'],
    //         'pin' => online_servant['service']['pin'],
    //             ],
    //         'location' => [
    //         'lat' => @location[1],
    //         'lng' => @location[0],
    //         'bearing' => online_servant['bearing']
    //             ]
    //             ]);
    //     }
    //     return data;
    // }
    //
    // public static double setOriginInput(request, lat, lng)
    // {
    //     requestoffsetSet('origin', [
    //         'lat' => lat,
    //     'lng' => lng
    //         ]);
    //     return request;
    // }
    //
    // public static double changeTimeZone(time, from_time_zone, to_time_zone): string
    // {
    //     return Carbon::parse(time, from_time_zone)
    //     setTimezone(to_time_zone)format('H:i:s');
    // }
    //
    // public static double changeDateTimeTimeZone(date_time, from_time_zone, to_time_zone, format = 'Y-m-d H:i:s'): string
    // {
    //     return Carbon::parse(date_time, from_time_zone)
    //     setTimezone(to_time_zone)format(format);
    // }
    //
    // public static double dateTimeToJalaliFormat(date_time)
    // {
    //     [year, month, day] =
    //     explode('-', explode(' ', date_time)[0]);
    //
    //     date = implode('-', Verta::getJalali(year, month, day));
    //     time = explode(' ', date_time)[1];
    //
    //     return [
    //     'date' => date, 'time' => time];
    // }
    //
    // public static double dateTimeFilter(request, query)
    // {
    //     querywhen(is_null(requeststart_at) and is_null(requestend_at), double(query) {
    //         return querywhere('created_at', '>=',  \Carbon\Carbon::now()startOfDay()toDateTimeString())
    //         where('created_at', '<=', Carbon::now()endOfDay()toDateTimeString());
    //     })when(!is_null(requeststart_at) and is_null(requestend_at), double(query) use(request) {
    //         return querywhereDate('created_at', '>=', requeststart_at)
    //         whereDate('created_at', '<=', Carbon::now()endOfDay()toDateTimeString());
    //     })when(is_null(requeststart_at) and !is_null(requestend_at), double(query) use(request) {
    //         return querywhereDate('created_at', '>=', Carbon::create(requestend_at)startOfDay()toDateTimeString())
    //         whereDate('created_at', '<=', requestend_at);
    //     })when(!is_null(requeststart_at) and !is_null(requestend_at), double(query) use(request) {
    //         return querywhereDate('created_at', '>=', requeststart_at)
    //         whereDate('created_at', '<=', requestend_at);
    //     });
    // }
    //
    // public static double statusFilter(request, query): void
    // {
    //     querywhen(!is_null(requeststatus), double(query) use(request) {
    //         querywhen(requeststatus == 0, double(query) {
    //             return querywhere('status', '<', 0);
    //         })when(requeststatus != 0, double(query) use(request) {
    //             return querywhere('status', requeststatus);
    //         });
    //     });
    // }
    //
    // public static double areaFilter(request, query, user): void
    // {
    //     querywhen(!is_null(requestarea_id), double(query) use(request) {
    //         return querywhereHas('request', double(query) use(request) {
    //             querywherehas('service_area_type.area', double(query) use(request) {
    //                 querywhere('id', requestarea_id);
    //             });
    //         });
    //     })
    //     when(is_null(requestarea_id), double(query) use(request, user) {
    //         areas = useremployeegetAreas();
    //         area_ids = areaspluck('id')toArray();
    //
    //         return querywhereHas('request', double(query) use(request, area_ids) {
    //             querywherehas('service_area_type.area', double(query) use(request, area_ids) {
    //                 querywhereIn('id', area_ids);
    //             });
    //         });
    //     });
    // }
    //
    // public static double requestAreaFilter(request, query, user)
    // {
    //     if (!is_null(requestarea_id))
    //     {
    //         return querywherehas('service_area_type.area', double(query) use(request) {
    //             querywhere('id', requestarea_id);
    //         });
    //     }
    //     else
    //     {
    //         areas = useremployeegetAreas();
    //         area_ids = areaspluck('id')toArray();
    //
    //         return querywherehas('service_area_type.area', double(query) use(area_ids) {
    //             querywhereIn('id', area_ids);
    //         });
    //     }
    // }
    //
    // public static double setServantListData(data)
    // {
    //     result =  []
    //     ;
    //     foreach (data as item){
    //         result[] =  [
    //         'id' => item['id'],
    //         'user' => [
    //         'id' => item['id'],
    //         'mobile' => item['user']['mobile'],
    //         'servant' => [
    //         'first_name' => @item['user']['servant']['first_name'],
    //         'last_name' => @item['user']['servant']['last_name'],
    //         'avatar' => @item['user']['servant']['avatar'],
    //             ],
    //             ],
    //         'distance' => item['distance']
    //             ];
    //     }
    //     return result;
    // }
    //
    // public static double setToRequest(inputs)
    // {
    //     foreach (inputs as key => value){
    //         request()offsetSet(key, value);
    //     }
    // }
}