import type { Schedule } from "./schedule";

export class Appointment {
    public id = 0;
    public scheduleId = 0;
    public schedule?: Schedule;
    public start = "";
    public end = "";
    public patientName = "";
    public description = "";
}