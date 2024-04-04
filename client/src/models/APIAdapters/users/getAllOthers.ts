import { Role } from "../../enums";

export class User {
  id = "";
  name = "";
  profilePictureUrl = "";
  email = "";
  createdAt = "";
  lastLogin = "";
  verifiedAt = "";
  role = Role.NONE;
}

export const getAllOthers = async (name?: string): Promise<Array<User>> => {
  let aux = name === undefined ? "" : name;
  return new Promise<Array<User>>(async (resolve) => {
    const response = await fetch(`/api/users/others?name=${aux}`);
    resolve(await response.json());
  });
};
