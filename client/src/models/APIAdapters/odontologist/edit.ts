import { Odontologist  } from "../../odontologist";
  
export const edit = (arg: Odontologist, token: string): Promise<Odontologist | null> => {
return new Promise<Odontologist | null>(async (resolve) => {
    const options = {
    method: "PUT",
    body: JSON.stringify({token: token, ...arg}),
    };
    const response = await fetch("/api/odontologist/", options);
    resolve(response.ok ? await response.json() : null);
});
};