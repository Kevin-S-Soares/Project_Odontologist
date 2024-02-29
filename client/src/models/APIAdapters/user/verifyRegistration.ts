export class VerifyRegistration {
  hash = "";
  userId = "";
  operation = 0;
}

export const verifyRegistration = (
  arg: VerifyRegistration,
): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "POST",
      body: JSON.stringify(arg),
    };
    const response = await fetch("/api/user/verify_registration", options);
    resolve(response.ok);
  });
};
