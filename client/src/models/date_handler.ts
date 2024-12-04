export class DateHandler {
  public static getDayOfTheWeek(arg: number): string {
    if (arg === 0) {
      return "Sunday";
    }
    if (arg === 1) {
      return "Monday";
    }
    if (arg === 2) {
      return "Tuesday";
    }
    if (arg === 3) {
      return "Wednesday";
    }
    if (arg === 4) {
      return "Thursday";
    }
    if (arg === 5) {
      return "Friday";
    }
    if (arg === 6) {
      return "Saturday";
    }
    return "";
  }
}
