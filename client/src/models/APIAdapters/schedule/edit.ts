import { Schedule  } from "../../schedule";
  
export const edit = (arg: Schedule): Promise<Schedule | null> => {
return new Promise<Schedule | null>(async (resolve) => {
    const options = {
    method: "PUT",
    body: JSON.stringify(arg),
    };
    const response = await fetch("/api/schedule/", options);
    resolve(response.ok ? await response.json() : null);
});
};