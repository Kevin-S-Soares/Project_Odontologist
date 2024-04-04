import type { APIRoute } from "astro";

export const GET: APIRoute = async ({ url, request }) => {
  const cookie = request.headers.get("cookie") ?? "";
  const matches = cookie.match(/Authorization=(bearer\s.*)/);
  const authorization = matches === null ? "" : matches[1];
  const name = url.searchParams.get("name") ?? "";
  const options = {
    headers: {
      "content-type": "application/json",
      authorization: authorization,
    },
  };
  const response = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/users/others?name=${name}`,
    options,
  );
  return new Response(JSON.stringify(await response.json()));
};
