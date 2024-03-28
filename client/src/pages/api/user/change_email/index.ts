import type { APIRoute } from "astro";

export const POST: APIRoute = async ({ request }) => {
  const requestBody = await request.json();
  const options = {
    headers: {
      "content-type": "application/json",
      authorization: `bearer ${requestBody["token"]}`,
    },
    method: "POST",
  };
  const email = requestBody["email"];
  const body = await fetch(
    `${process.env.SERVER ?? ""}/api/v1/user/change_email?email=${email}`,
    options,
  );
  return new Response(JSON.stringify(body.ok), {
    status: body.status,
  });
};
