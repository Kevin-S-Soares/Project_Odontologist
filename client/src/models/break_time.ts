import type { Schedule } from "./schedule";

export class BreakTime {
  public id = 0;
  public scheduleId = 0;
  public schedule?: Schedule;
  public name = "";
  public startTime = "";
  public startDay = 0;
  public endTime = "";
  public endDay = 0;
}
