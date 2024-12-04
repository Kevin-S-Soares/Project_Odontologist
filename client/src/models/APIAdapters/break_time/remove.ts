export const remove = (id: number, token: string): Promise<boolean | null> => {
  return new Promise<boolean | null>(async (resolve) => {
    const options = {
      method: "DELETE",
      body: JSON.stringify({ id: id, token: token }),
    };
    const response = await fetch("/api/break_time/", options);
    resolve(response.ok ? await response.json() : null);
  });
};
