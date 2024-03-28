import { Role } from "../../enums";

export class UpdateUser {
  id = "";
  name = "";
  role = Role.NONE;
}

export const update = (user: UpdateUser, token: string): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "PUT",
      body: JSON.stringify({
        id: user.id,
        name: user.name,
        role: user.role,
        token: token,
      }),
    };
    const response = await fetch("/api/user/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
