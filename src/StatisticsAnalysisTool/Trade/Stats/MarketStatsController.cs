using StatisticsAnalysisTool.Network.Manager;

namespace StatisticsAnalysisTool.Trade.Stats;

public class MarketStatsController
{
    private readonly TrackingController _trackingController;
    
    public MarketStatsController(TrackingController trackingController)
    {
        _trackingController = trackingController;
    }
}