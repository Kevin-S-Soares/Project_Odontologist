export class User {
  name = "";
  email = "";
  password = "";
}

export const register = (arg: User): Promise<User | null> => {
  return new Promise<User | null>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(arg),
    };
    const response = await fetch("/api/user/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
