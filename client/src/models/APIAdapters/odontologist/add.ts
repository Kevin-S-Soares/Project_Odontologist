import { Odontologist  } from "../../odontologist";
  
export const add = (arg: Odontologist, token: string): Promise<Odontologist | null> => {
return new Promise<Odontologist | null>(async (resolve) => {
    const options = {
    method: "POST",
    body: JSON.stringify({token: token, ...arg}),
    };
    const response = await fetch("/api/odontologist/", options);
    resolve(response.ok ? await response.json() : null);
});
};