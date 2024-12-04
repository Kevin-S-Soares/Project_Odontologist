import { Odontologist } from "../../odontologist";

export const add = (arg: Odontologist): Promise<Odontologist | null> => {
  return new Promise<Odontologist | null>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(arg),
    };
    const response = await fetch("/api/odontologist/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
