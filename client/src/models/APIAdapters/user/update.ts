import { Role } from "../../enums";

export class UpdateUser {
  id = "";
  name = "";
  role = Role.NONE;
}

export const update = (user: UpdateUser, token: string): Promise<string> => {
  return new Promise<string>(async () => {
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
    if (response.ok) {
      window.location.replace("/user/refresh_token");
    }
  });
};
