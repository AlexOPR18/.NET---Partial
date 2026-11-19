# DECISIONS

Answer **yourself**, in your own words, in 3–5 sentences per question.
Refer to concrete files and classes from your own repo (for example `Features/Bookings/CreateBooking/...`).
An unanswered question gives 0 points.

## 1. A rule you moved
Pick one business rule (R1–R8). In which file did it live in the starter and in which file does it live now? Why there?

R7. It used to be in Services/BookingService.cs, inside a foreach with the wrong condition. Now it’s in Domain/Bookings/TimeRange.cs (the Overlaps method) and in Room.Book. Why there: the room knows its own bookings, and the rule is written once and can’t be bypassed.


## 2. Result instead of an exception
Why is `Result` a better choice than an exception for "the room is occupied"? What happens in your code when an `Error` is returned?

The room being taken is a normal case, not something unexpected. The Result<Booking> signature tells you the method can fail. Here’s what happens: the handler returns the Error, and the endpoint calls ToProblem(), which uses the ErrorType to return a 400, 404 or 409 as ProblemDetails.

## 3. Who knows about EF Core?
Which classes in your project know about EF Core and which do not? What do you gain and what do you pay with Repository + Unit of Work?

EF Core: only the classes in Infrastructure/Persistence know about it (the DbContext, the configurations, the repositories and the converter). Domain and Features don’t. What you gain: handlers that don’t depend on the database. What you pay: more code, and it’s harder to control the SQL.

## 4. The flow of a request
Draw in text (ASCII arrows) the path of a `POST /api/rooms/{roomId}/bookings` request through your project, with the **real** names of files and classes, from the endpoint to the database.

CreateBookingEndpoint (builds the CreateBookingCommand) -> CreateBookingHandler -> IRoomRepository.GetByIdAsync (404 if the room doesn’t exist) -> IBookingRepository.GetActiveForRoomAsync -> Room.Book (which calls Booking.Create and TimeRange.Create) -> GetActiveForParticipantOnDayAsync and Booking.EnsureWithinDailyLimit (R9) -> IBookingRepository.Add, then IUnitOfWork.SaveChangesAsync, meaning RoomHubDbContext, then SQLite -> the response: 201 with Location

## 5. What you rejected from what the AI gave you
Give a concrete example of code or an idea proposed by the AI that you corrected or rejected, and say why.

Două cereri simultane pot trece amândouă de verificarea suprapunerii, pentru că nu există blocare (concurență).

## 6. If you had 30 more minutes
What would you improve and what risk did you leave in the project?

Automated tests. Unit tests for Room.Book, Booking.Cancel and TimeRange.Overlaps. The rules must be verified automatically, not just manually from romhub.http


