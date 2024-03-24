export const changeEmail = (email: string, token: string): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify({ email: email, token: token }),
    };
    const response = await fetch("/api/user/change_email/", options);
    resolve(response.ok);
  });
};
