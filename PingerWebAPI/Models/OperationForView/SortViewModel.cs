using static PingerAPI.Models.Enums.SortingEnum;

namespace PingerAPI.Models.OperationForView
{
    public class SortViewModel
    {
        public SortState VINSort { get; }
        public SortState OrderNumberSort { get; }
        public SortState StandNameSort { get; }
        public SortState OperatorSort { get; }
        public SortState DateSort { get; }
        public SortState Current { get; }

        public SortViewModel(SortState sortOrder)
        {
            VINSort = sortOrder == SortState.VINAsc ? SortState.VINDesc : SortState.VINAsc;
            OrderNumberSort = sortOrder == SortState.OrderNumberAsc ? SortState.OrderNumberDesc : SortState.OrderNumberAsc;
            StandNameSort = sortOrder == SortState.StandNameAsc ? SortState.StandNameDesc : SortState.StandNameAsc;
            OperatorSort = sortOrder == SortState.OperatorAsc ? SortState.OperatorDesc : SortState.OperatorAsc;
            DateSort = sortOrder == SortState.DateAsc ? SortState.DateDesc : SortState.DateAsc;
            Current = sortOrder;
        }
    }
}