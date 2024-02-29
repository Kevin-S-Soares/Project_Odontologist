export const forgetPassword = (email: string): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(email),
    };
    const response = await fetch("/api/user/forget_password/", options);
    resolve(response.ok);
  });
};
