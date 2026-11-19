# BUGS

The starter contains behaviors that contradict `ASSIGNMENT.md`. Write here every discrepancy you found.
Fill it in yourself, in your own words. There is no required number: what counts is that they are real.

| Which rule from ASSIGNMENT.md is violated (R1–R8) | Where the problem was in the starter (file, approximate line) | How you discovered it (test, reading, AI) | What you changed |
|---|---|---|---|
| R7 | `Services/BookingService.cs`, line 28 | The AI pointed it out when comparing the code with the rules. I checked it with a booking from 12:00 to 13:00 right after one from 10:00 to 12:00 | Overlap now uses `<` in `TimeRange.Overlaps`, so two bookings that only touch are fine |
| R7, R8 | `Services/BookingService.cs` line 26 and `Repositories/BookingRepository.cs` line 27 | Reading the code. The loop also looked at cancelled bookings. I tested it by cancelling a booking and booking the same hours again | Only active bookings are loaded (`GetActiveForRoomAsync`) and `Booking.Blocks` checks `IsActive` |
| R8 | `Services/BookingService.cs`, lines 56-59 | Test: I cancelled the same booking twice and got 204 both times | `Booking.Cancel` returns `AlreadyCancelled` (409) if the booking is not active anymore |
| R5 | `Services/BookingService.cs`, `Create` | Reading the rules list. There was no check for this | `Booking.Create` returns `StartInPast` (400). "Now" comes from `TimeProvider` |
| R6 | `Services/BookingService.cs`, `Create` | Reading the rules list. There was no check for this | `Booking.Create` returns 400 if the booking is longer than 4 hours. Exactly 4 hours still works |
