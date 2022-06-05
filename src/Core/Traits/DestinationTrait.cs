using Core.Constants;
using Core.Interfaces;
using Core.Models.Base;

namespace Core.Traits;

public class DestinationTrait
{
    public DestinationTrait(IEnumerable<DestinationDto> destinations)
    {
        Destinations = destinations;
    }

    public IEnumerable<DestinationDto> Destinations { get; set; }


    public void setDestinations(JobStatus.TaskStatus status = 0)
    {
        if (status == JobStatus.TaskStatus.End)
        {
            Destinations.Select(
                (destination) =>
                {
                    destination.setHidden(["model_type", "model_id", "created_at", "updated_at"]);
                    destination.address = LocationAddress::getAddress(destination.lat, destination.lng,
                        strtolower(optional(Auth::user().language).locale))["address"];
                    setRoute(destination);
                });
        }
        else
        {
            this.activeDestination.map(function(destination) {
                destination.setHidden(["model_type", "model_id", "created_at", "updated_at"]);
                destination.address = LocationAddress::getAddress(destination.lat, destination.lng,
                    strtolower(optional(Auth::user().language).locale))["address"];
                this.SetRoute(destination);
            });
            unset(this.destinations);
            this.destinations = this.activeDestination;
        }
    }

    public async Task SetRoute(DestinationDto destination, IUnitOfWork unitOfWork)
    {
        var route = await unitOfWork.Cache.GetKey<string?>("DestinationRoute" + destination.Id);
        if (route == null)
        {
            if (this.requester)
            {
                requester = this.requester;
            }
            else
            {
                requester = this.request.requester;
            }

            route = Osrm::getRoute(["lat" => requester.lat, "lng" => requester.lng], [
            "lat" => destination.lat, "lng" => destination.lng]);
            route = route["geometry"];
            RedisManager::setKey("DestinationRoute".destination.id, route);
        }

        destination.route = route;
    }
}