import { Odontologist  } from "../../odontologist";
  
export const edit = (arg: Odontologist): Promise<Odontologist | null> => {
return new Promise<Odontologist | null>(async (resolve) => {
    const options = {
    method: "PUT",
    body: JSON.stringify(arg),
    };
    const response = await fetch("/api/odontologist/", options);
    resolve(response.ok ? await response.json() : null);
});
};