export const changePassword = (
  password: string,
  token: string,
): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify({ password: password, token: token }),
    };
    const response = await fetch("/api/user/change_password/", options);
    resolve(response.ok);
  });
};
