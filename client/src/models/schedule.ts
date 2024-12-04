import type { Odontologist } from "./odontologist";

export class Schedule {
  public id = 0;
  public odontologistId = 0;
  public odontologist? : Odontologist;
  public name = "";
  public startDay = 0;
  public startTime = "";
  public endDay = 0;
  public endTime = "";
}
