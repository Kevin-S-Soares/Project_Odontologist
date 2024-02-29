export class Authentication {
  email = "";
  password = "";
}

export const authenticate = (arg: Authentication): Promise<string> => {
  return new Promise<string>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(arg),
    };
    const response = await fetch("/api/user/authenticate", options);
    resolve(response.ok ? await response.json() : "");
  });
};
