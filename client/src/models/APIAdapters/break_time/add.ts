import { BreakTime } from "../../break_time";

export const add = (arg: BreakTime): Promise<BreakTime | null> => {
  return new Promise<BreakTime | null>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(arg),
    };
    const response = await fetch("/api/break_time/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
