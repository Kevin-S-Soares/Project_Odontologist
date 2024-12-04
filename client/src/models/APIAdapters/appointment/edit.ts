import { Appointment } from "../../appointment";

export const edit = (arg: Appointment, token: string): Promise<Appointment | null> => {
  return new Promise<Appointment | null>(async (resolve) => {
    const options = {
      method: "PUT",
      body: JSON.stringify({ token: token, ...arg }),
    };
    const response = await fetch("/api/appointment/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
