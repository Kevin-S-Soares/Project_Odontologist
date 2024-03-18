export const remove = (id: string, token: string): Promise<boolean> => {
  return new Promise<boolean>(async (resolve) => {
    const options = {
      method: "DELETE",
      body: JSON.stringify({ id: id, token: token }),
    };
    const response = await fetch("/api/user/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
