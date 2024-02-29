export class ResetPassword {
  userId = "";
  hash = "";
  operation = 0;
  password = "";
}

export const resetPassword = (arg: ResetPassword): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(arg),
    };
    const response = await fetch("/api/user/reset_password/", options);
    resolve(response.ok);
  });
};
