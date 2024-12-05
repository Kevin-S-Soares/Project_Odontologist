import type { Appointment } from "../../appointment";

export const add = (
  arg: Appointment,
  token: string,
): Promise<Appointment | null> => {
  return new Promise<Appointment | null>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify({ token: token, ...arg }),
    };
    console.log(options.body);
    const response = await fetch("/api/appointment/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
