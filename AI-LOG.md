# AI-LOG

Write down, in order, what you asked the AI. It is not a punishment: a missing or false log lowers the level D grade.

| Time (approx.) | What you asked (briefly) | What you kept | What you changed or rejected |
|---|---|---|---|
| 2 min | The initialization of work load | Time management instructions | Nothing |
| 8 min | Transformation of the code in Slices, Rich Domain, Result, Handlers | Features/ structure, Room.Book, TimeRange | Nothing |
| 11 min | EF Core with SQLite, repository and Unit of Work | DbContext as IUnitOfWork, Configurations | Nothing |
| 5 min | New Requirement R9 | Booking Rule, new method in Repository | Nothing |
| 10 min | New Requirement R10 and R11 | R10 in Room.Book (IsLarge, 2-hour limit), R11 in Booking.Cancel(nowUtc) with the time from TimeProvider via CancelBookingHandler, new errors DurationTooLongForLargeRoom (400) and CancellationTooLate (409).| Nothing |
