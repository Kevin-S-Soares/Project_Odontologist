import { BreakTime } from "../../break_time";

export const edit = (
  arg: BreakTime,
  token: string,
): Promise<BreakTime | null> => {
  return new Promise<BreakTime | null>(async (resolve) => {
    const options = {
      method: "PUT",
      body: JSON.stringify({ token: token, ...arg }),
    };
    const response = await fetch("/api/break_time/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
